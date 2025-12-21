// Copyright (c) 2011 Matthew D Martin
// MIT or MS-PL License as you elect, please see root directory for text of licenses.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Hicmah.Administration
{
    public abstract class DatabaseAdministration
    {
        public abstract void CreateTables();

        public abstract void SeedData();

        //Precalc things
        //Maybe also precalc additional measures
        public abstract void ProcessAllDiminsions();

        //Removing Data, esp for test and development
        public abstract void ClearData();

        public abstract void ClearCache();

        //Move data to archive.  Will be accessible by using table prefix swapping.
        public abstract void ArchiveData();

        //For bulk loading of test data
        public abstract void DropIndexes();

        //Or else queries will be slow
        public abstract void CreateIndexes();

        //For bulk loading of test data
        public abstract void DropKeys();

        //For bulk loading of test data
        public abstract void CreateKeys();

        //Load the live operational data (a table with fast inserts but slow reads)
        //into a highly indexed reporting table (which has very slow inserts, but fast reads)
        public abstract void CreateReportingStructure();

        public abstract void Compact();
        public abstract TableInfo TableInfo();

    }
}
