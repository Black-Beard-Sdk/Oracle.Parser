using Newtonsoft.Json;

namespace Bb.Oracle.Models
{

    public abstract class OracleObject
    {

        [JsonIgnore]
        public abstract KindModelEnum KindModel { get; }

    }

    public enum KindModelEnum
    {

        Undefined,

        Sequence,
        Index,
        UserObjectPrivilege,
        Constraint,
        Package,
        PackageBodies,
        Procedure,
        Table,
        Trigger,
        Type,
        View,
        Jobs,
        MaterializedView,
        MaterializedViewLog,
        Tablespace,
        Cluster,
        DatabaseLink,
        Directory,
        IndexPartition,
        IndexSubPartition,
        IndexType,
        Lob,
        LobPartition,
        Operator,
        Queue,
        Rule,
        Schedule,
        TablePartition,
        TablesubPartition,
        Function,
        Column,
        Property,
        Privilege,
        BlocPartition,
        Partition,
        Argument,
        ForeignKey,
        ConstraintColumn,
        IndexColumn,
        OracleType,
        PartitionColumn,
        PartitionRef,
        Schemas,
        SubPartition,
        Synonym,

        UnaryExpression,
        BinaryExpression,
        TierceExpression,
        Constant,
        CallProcedure,
        VariableDeclaration,
        VariableReference,
        MethodArgument,
        TypeReference,

        NotImplemented,
        TableTypeRef,
        ArrayTypeDef,
        RecordTypeDef,
        RefCursorTypeDef,
        CursorDeclaration,
        TableIndexedByPart,
        TableTypeDefinition,
        FieldSpec,
        VarrayTypeDef,
        CreateOrReplace,
        Alter,
        Drop,
        TableComment,
        TableColumnComment,
        PhysicalAttribute,
    }

}