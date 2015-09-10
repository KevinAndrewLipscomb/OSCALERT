START TRANSACTION
;
update field_situation_impression
set elaboration = REPLACE(elaboration,'Assgnmt=','http://fp2w.net Assgnmt=')
;
update field_situation_impression
set elaboration = concat(elaboration,' http://fp2w.net')
where elaboration not like '%http://fp2w.net%'
;
COMMIT
