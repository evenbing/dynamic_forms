using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.DynamicForms.Models;

namespace Orchard.DynamicForms {
    public class Migrations : DataMigrationImpl {

        public int Create()
        {
            //Creating Table DynamicFormPartRecord
            SchemaBuilder.CreateTable("DynamicFormPartRecord", table => table
               .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
               .Column("Enabled", DbType.Boolean)
           );


            ContentDefinitionManager.AlterPartDefinition(
           typeof(DynamicFormPart).Name, cfg => cfg.Attachable());

            // Creating table DynamicFormFieldsRecord
            SchemaBuilder.CreateTable("DynamicFormFieldsRecord", table => table
                .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                .Column("Name", DbType.String)
                .Column("DropZoneID", DbType.String)
                .Column("State", DbType.String, column => column.Unlimited())
                .Column("ContentID", DbType.Int32)
            );

            // Creating table DynamicFormResponseRecord
            SchemaBuilder.CreateTable("DynamicFormResponseRecord", table => table
               .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
               .Column("Response", DbType.String)
               .Column("DynamicFormRecord_Id", DbType.Int32)
            );

            return 1;
        }


        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterTypeDefinition("DynamicForm", builder => builder
                       .DisplayedAs("Dynamic Form")
                       .WithPart("DynamicFormPart")
                       .WithPart("CommonPart")
                       .WithPart("TitlePart")
                       .WithPart("BodyPart")
                       .Creatable()
                       .Draftable());



            return 2;

        }

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterTypeDefinition("DynamicForm", builder => builder
                        .DisplayedAs("Dynamic Form")
                        .WithPart("DynamicFormPart")
                        .WithPart("CommonPart")
                        .WithPart("TitlePart")
                        .WithPart("BodyPart"));

            return 3;
        }

    }
}