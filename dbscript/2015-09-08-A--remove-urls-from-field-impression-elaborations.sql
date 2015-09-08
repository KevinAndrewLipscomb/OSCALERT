START TRANSACTION
;
update field_situation_impression
set elaboration = REPLACE(elaboration,' http://goo.gl/lvMvXs','')
;
COMMIT
