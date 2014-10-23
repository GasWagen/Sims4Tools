﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using s4pi.Interfaces;

namespace CatalogResource.TS4
{
    public class WallCatalogResource : ObjectCatalogResource
    {
        private MATDList matdList;
        private ThumbnailList thumList;
        private uint unknown1;
        private SwatchColorList colorList;
        private ulong catalogGroupID;

        public WallCatalogResource(int APIversion, Stream s) : base(APIversion, s) { }

        protected override void Parse(Stream s)
        {
            BinaryReader r = new BinaryReader(s);
            base.Parse(s);
            this.matdList = new MATDList(OnResourceChanged, s);
            this.thumList = new ThumbnailList(OnResourceChanged, s);
            this.unknown1 = r.ReadUInt32();
            this.colorList = new SwatchColorList(OnResourceChanged, s);
            this.catalogGroupID = r.ReadUInt64();
        }

        protected override Stream UnParse()
        {
            var s =  base.UnParse();
            BinaryWriter w = new BinaryWriter(s);
            if (this.matdList == null) this.matdList = new MATDList(OnResourceChanged);
            matdList.UnParse(s);
            if (this.thumList == null) this.thumList = new ThumbnailList(OnResourceChanged);
            this.thumList.UnParse(s);
            w.Write(this.unknown1);
            if (this.colorList == null) this.colorList = new SwatchColorList(OnResourceChanged);
            this.colorList.UnParse(s);
            w.Write(this.catalogGroupID);
            return s;
        }
        [ElementPriority(15)]
        public MATDList MatdList { get { return this.matdList; } set { if (!this.matdList.Equals(value)) { OnResourceChanged(this, EventArgs.Empty); this.matdList = value; } } }
        [ElementPriority(16)]
        public ThumbnailList ThumList { get { return this.thumList; } set { if (!this.thumList.Equals(value)) { OnResourceChanged(this, EventArgs.Empty); this.thumList = value; } } }
        [ElementPriority(17)]
        public uint Unknown1 { get { return this.unknown1; } set { if (!this.unknown1.Equals(value)) { OnResourceChanged(this, EventArgs.Empty); this.unknown1 = value; } } }
        [ElementPriority(18)]
        public SwatchColorList ColorList { get { return this.colorList; } set { if (!this.colorList.Equals(value)) { OnResourceChanged(this, EventArgs.Empty); this.colorList = value; } } }
        [ElementPriority(19)]
        public ulong CatalogGroupID { get { return this.catalogGroupID; } set { if (!this.catalogGroupID.Equals(value)) { OnResourceChanged(this, EventArgs.Empty); this.catalogGroupID = value; } } }
    }
}