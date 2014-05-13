START TRANSACTION
;
ALTER TABLE `cad_record` ADD INDEX `be_current` (`be_current` ASC)
;
COMMIT