REM
REM $Id$
REM
cd "C:\Inetpub\wwwroot\OSCALERT"
start /max explorer /e,/select,C:\Inetpub\wwwroot\OSCALERT\.svn
start /max OSCALERT.sln
IF EXIST "C:\Program Files\MySQL\MySQL Workbench\MySQLWorkbench.exe" (start "" /max "C:\Program Files\MySQL\MySQL Workbench\MySQLWorkbench.exe") ELSE start "" /max "C:\Program Files (x86)\MySQL\MySQL Workbench\MySQLWorkbench.exe"
