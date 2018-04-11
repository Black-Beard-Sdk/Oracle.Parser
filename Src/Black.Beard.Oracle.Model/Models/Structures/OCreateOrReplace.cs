using System;
using System.Collections.Generic;
using System.Text;
using Bb.Oracle.Models;

namespace Bb.Oracle.Structures.Models
{

    public class OCreateOrReplace : ItemBase
    {

        public override KindModelEnum KindModel => KindModelEnum.CreateOrReplace;

        public ItemBase Item { get; set; }

        public override string GetName() { return this.Item.GetName(); }

        public override string GetOwner() { return this.Item.GetOwner(); }

        public override void Initialize()
        {
            this.Item.Initialize();
        }

    }

    public class OAlter : ItemBase
    {

        public override KindModelEnum KindModel => KindModelEnum.Alter;

        public ItemBase Item { get; set; }

        public override string GetName() { return this.Item.GetName(); }

        public override string GetOwner() { return this.Item.GetOwner(); }

        public override void Initialize()
        {
            this.Item.Initialize();
        }

    }

    public class ODrop : ItemBase
    {

        public override KindModelEnum KindModel => KindModelEnum.Drop;

        public ItemBase Item { get; set; }

        public override string GetName() { return this.Item.GetName(); }

        public override string GetOwner() { return this.Item.GetOwner(); }

        public override void Initialize()
        {
            this.Item.Initialize();
        }

    }
}
