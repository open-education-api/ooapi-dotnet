----------------
-- Get Attributes for EducationSpecifications
----------------
MERGE ooapiv5.EducationSpecifications AS ESD
USING (
    SELECT distinct es.EducationSpecificationId
      ,
      (SELECT attr.Value as Value, attr.PropertyName as PropertyName, attr.Language as Language
        FROM ooapiv5.Attributes attr
        where attr.EducationSpecificationId = es.EducationSpecificationId
        FOR JSON AUTO
      ) as jsonvalue

  FROM ooapiv5.EducationSpecifications es, ooapiv5.Attributes attr
  where es.EducationSpecificationId = attr.EducationSpecificationId
)as ESS
 ON ESD.EducationSpecificationId = ESS.EducationSpecificationId
WHEN MATCHED THEN
UPDATE SET 
ESD.Attributes = ESS.jsonvalue;
----------------


----------------
-- Get Consumers for EducationSpecifications
----------------
MERGE ooapiv5.EducationSpecifications AS ESD
USING (
SELECT distinct es.EducationSpecificationId
      ,
      (SELECT cons.ConsumerKey as ConsumerKey, cons.PropertyType as PropertyType, cons.PropertyName as PropertyName, cons.PropertyValue as PropertyValue
        FROM ooapiv5.Consumers cons
        where cons.EducationSpecificationId = es.EducationSpecificationId
        FOR JSON AUTO
      ) as jsonvalue

  FROM ooapiv5.EducationSpecifications es, ooapiv5.Consumers cons
  where es.EducationSpecificationId = cons.EducationSpecificationId

)as ESS
 ON ESD.EducationSpecificationId = ESS.EducationSpecificationId
WHEN MATCHED THEN
UPDATE SET 
ESD.Consumers = ESS.jsonvalue;
----------------




----------------
-- Get Attributes for Programs
----------------
MERGE ooapiv5.Programs AS PRD
USING (
    SELECT distinct prs.ProgramId
      ,
      (SELECT attr.Value as Value, attr.PropertyName as PropertyName, attr.Language as Language
        FROM ooapiv5.Attributes attr
        where attr.ProgramId = prs.ProgramId
        FOR JSON AUTO
      ) as jsonvalue

  FROM ooapiv5.Programs prs, ooapiv5.Attributes attr
  where prs.ProgramId = attr.ProgramId
)as PRS
 ON PRD.ProgramId = PRS.ProgramId
WHEN MATCHED THEN
UPDATE SET 
PRD.Attributes = PRS.jsonvalue;
----------------


----------------
-- Get Consumers for Programs
----------------
MERGE ooapiv5.Programs AS PRD
USING (
SELECT distinct prs.ProgramId
      ,
      (SELECT cons.ConsumerKey as ConsumerKey, cons.PropertyType as PropertyType, cons.PropertyName as PropertyName, cons.PropertyValue as PropertyValue
        FROM ooapiv5.Consumers cons
        where cons.ProgramId = prs.ProgramId
        FOR JSON AUTO
      ) as jsonvalue

  FROM ooapiv5.Programs prs, ooapiv5.Consumers cons
  where prs.ProgramId = cons.ProgramId

)as PRS
 ON PRD.ProgramId = PRS.ProgramId
WHEN MATCHED THEN
UPDATE SET 
PRD.Consumers = PRS.jsonvalue;
----------------

