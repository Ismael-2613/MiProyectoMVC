#!/bin/bash
sleep 30
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P MiPassword123! -Q "
RESTORE DATABASE ProyectoMVC
FROM DISK = '/var/opt/mssql/backup/ProyectoSO.bak'
WITH MOVE 'ProyectoMVC' TO '/var/opt/mssql/data/ProyectoMVC.mdf',
     MOVE 'ProyectoMVC_log' TO '/var/opt/mssql/data/ProyectoMVC_log.ldf',
     REPLACE;
ALTER AUTHORIZATION ON DATABASE::ProyectoMVC TO sa;
"