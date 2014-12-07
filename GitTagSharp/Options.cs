using CommandLine;

namespace GitTagSharp
{
  public class Options
  {
    [Option('r', "read", Required = true, HelpText = "Repository name to be tagged.")]
    public string Repository { get; set; }

    [Option('l', "localRepoPath", Required = false, DefaultValue = "C:\\temp", HelpText = "Local repository path.")]
    public string LocalRepoPath { get; set; }

    [Option('u', "userName", Required = true, HelpText = "UserName to connect to the remote repository.")]
    public string UserName { get; set; }

    [Option('p', "password", Required = true, HelpText = "Password to connect to the remote repository.")]
    public string Password { get; set; }

    [Option('e', "email", Required = true, HelpText = "Email to connect to the remote repository.")]
    public string Email { get; set; }

    [Option('b', "branch", Required = true, HelpText = "Branch name of the remote repository to be tagged.")]
    public string Branch { get; set; }

    [Option('t', "tagName", Required = true, HelpText = "Tag name to be applied to the remote repository.")]
    public string TagName { get; set; }
  }
}