using System;
using System.Linq;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace GitTagSharp
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var result = CommandLine.Parser.Default.ParseArguments<Options>(args);
      if (!result.Errors.Any())
      {
        string url = result.Value.Repository; 

        var brancOption = result.Value.Branch;
        var branchSepIndex = brancOption.IndexOf('/');
        if (branchSepIndex > 0)
        {
          var credentials = new UsernamePasswordCredentials
            {
              Username = result.Value.UserName,
              Password = result.Value.Password
            };
          CredentialsHandler credHandler = (s, fromUrl, types) => credentials;

          var remote = brancOption.Substring(0, branchSepIndex);
          var branch = brancOption.Substring(branchSepIndex+1, brancOption.Length - branchSepIndex-1);

          var workingDirectory = result.Value.LocalRepoPath;

          var isLocalRepoExist = Repository.Discover(workingDirectory);
          if (isLocalRepoExist == null)
          {
            var cloneOptions = new CloneOptions {CredentialsProvider = credHandler};
            Repository.Clone(url, workingDirectory, cloneOptions);
          }

          Repository repo = null;
          try
          {
            var tagName = result.Value.TagName;
            repo = new Repository(workingDirectory);
            //repo.Fetch(remote, new FetchOptions(){CredentialsProvider = credHandler});
            repo.Network.Pull(new Signature(result.Value.UserName,result.Value.Email, new DateTimeOffset()),
              new PullOptions() { FetchOptions = new FetchOptions() { CredentialsProvider = credHandler } });
            repo.ApplyTag(tagName);
            PushChanges(repo, credHandler, remote, branch, tagName);
            Console.WriteLine("Tagged :{0}", result.Value.TagName);
          }
          catch (Exception ex)
          {
            Console.WriteLine("Error happened {0}", ex.Message);
          }
          finally
          {
            if (repo != null) repo.Dispose();
          }
        }
      }
      Console.ReadLine();
    }

    public static void PushChanges(Repository repository, CredentialsHandler credentialsHandler, string remote, string branch, string tagName)
    {
      try
      {
        var remotes = repository.Network.Remotes[remote];
        var options = new PushOptions {CredentialsProvider = credentialsHandler};
        string pushRefSpec = string.Format("refs/heads/{0}:refs/tags/{1}", branch, tagName);
        repository.Network.Push(remotes, pushRefSpec, options);
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception:RepoActions:PushChanges " + e.Message);
      }
    }
  }
}