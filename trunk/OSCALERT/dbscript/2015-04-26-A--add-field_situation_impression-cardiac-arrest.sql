START TRANSACTION
;
insert ignore field_situation_impression
set description = 'CardiacArrest'
, elaboration = 'OSCALERT: CardiacArrest, <address/>. SAFETY *UNKNOWN*. http://goo.gl/lvMvXs Assgnmt=<assignment/>.'
, pecking_order = 1800
;
COMMIT