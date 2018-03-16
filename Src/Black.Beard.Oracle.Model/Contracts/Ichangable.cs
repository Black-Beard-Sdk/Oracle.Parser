using Bb.Oracle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bb.Oracle.Contracts
{

    public interface Ichangable
    {

        void Create(IchangeVisitor visitor);

        void Drop(IchangeVisitor visitor);

        void Alter(IchangeVisitor visitor, Ichangable source, string propertyName);

        IEnumerable<Anomaly> Evaluate(IEvaluateManager manager);

        KindModelEnum KindModel { get; }

        string GetOwner();

        string GetName();

        object Tag { get; }
    }

}
