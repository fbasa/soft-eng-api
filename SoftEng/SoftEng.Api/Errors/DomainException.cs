﻿using SoftEng.Domain.Exception;
using System.Runtime.Serialization;

namespace SoftEng.Api.Errors;

[Serializable]
public class DomainException : System.Exception
{
    public string Code { get; }
    public int Status { get; }

    public DomainException(string message, string code, int status = 422, System.Exception? inner = null)
        : base(message, inner) { Code = code; Status = status; }

    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Code = info.GetString(nameof(Code)) ?? ErrorCodes.Unknown;
        Status = info.GetInt32(nameof(Status));
    }
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Code), Code);
        info.AddValue(nameof(Status), Status);
    }
}