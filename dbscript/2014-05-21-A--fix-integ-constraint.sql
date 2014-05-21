START TRANSACTION
;
ALTER TABLE `field_situation` DROP FOREIGN KEY `field_situation_case_num`
;
ALTER TABLE `field_situation` 
  ADD CONSTRAINT `field_situation_case_num` FOREIGN KEY (`case_num` ) REFERENCES `cad_record` (`incident_num` )
  ON DELETE CASCADE
  ON UPDATE RESTRICT
;
COMMIT