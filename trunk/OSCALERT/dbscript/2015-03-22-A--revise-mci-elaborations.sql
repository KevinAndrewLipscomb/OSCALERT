START TRANSACTION
;
update field_situation_impression
set elaboration = REPLACE(elaboration,'MCI case active. Volunteers to your stations','MCI (Volunteers to your stations!) or Special Event (disregard) case active')
where description like 'Mci%'
;
COMMIT