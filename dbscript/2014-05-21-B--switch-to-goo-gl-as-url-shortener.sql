START TRANSACTION
;
update field_situation_impression
set elaboration = REPLACE(elaboration,'http://v.gd/7bfLG6','http://goo.gl/lvMvXs')
;
COMMIT