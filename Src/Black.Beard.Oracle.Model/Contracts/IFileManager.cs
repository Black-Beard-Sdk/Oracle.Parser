using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Oracle.Contracts
{

    public interface IFileManager
    {

        void Write(Ichangable item, IchangeVisitor visitor);

        void StartNewFile(Ichangable item, IchangeVisitor visitor);

        void StartBlock(KindModelEnum kindModelEnum);


        void CloseBlock(KindModelEnum kindModelEnum);

    }

}
