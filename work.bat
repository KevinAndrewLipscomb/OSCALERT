REM
REM $Id$
REM
cd "C:\Inetpub\wwwroot\OSCALERT"
start /max explorer /e,/select,C:\Inetpub\wwwroot\OSCALERT\.svn
start /max OSCALERT.sln
start "" /max "C:\Program Files\MySQL\MySQL Workbench CE 5.2.47\MySQLWorkbench.exe"
