git-tag-sharp
=============

Application to tag remote git repository via [libgit2sharp](https://github.com/libgit2/libgit2sharp)


**Usage**

`> GitTagSharp -r "remote repository url" -l "localpath to save the repo" -u "username" -p password -e "email" -b "orgin/master" -t "tagName" `

***Remote Repository*** 
GitHub repository url. eg:- https://github.com/Saanch/git-tag-sharp.git

***LocalPath***
Local machine temporary path to save the repository. It can be like `C:\temp`

***UserName***
Username to connect to the remote repository to fetch and push the changes.

***Password***
Password to connect to the remote repository to fetch and push the changes.

***Email***
Email to connect to the remote repository to fetch and push the changes.

***Branch***
The remote branch to be updated with the tag. eg:- `origin/master`

***TagName***
TagName to be added to the remote repository.
