START TRANSACTION
;
set foreign_key_checks = 0
;
UPDATE `field_situation_impression` SET `pecking_order`='3501' WHERE description = 'AmbNeeded' -- frees up 3000
;
UPDATE `field_situation_impression` SET `pecking_order`='3000' WHERE description = 'AlsNeeded' -- frees up 3500
;
UPDATE `field_situation_impression` SET `pecking_order`='3500' WHERE description = 'AmbNeeded'
;
set foreign_key_checks = 1
;
update member
set min_oscalert_peck_order_general = 3500
where min_oscalert_peck_order_general = 3000
;
update member
set min_oscalert_peck_order_als = 3000
where min_oscalert_peck_order_als = 3500
;
COMMIT