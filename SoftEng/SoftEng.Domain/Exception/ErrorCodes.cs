﻿namespace SoftEng.Domain.Exception;

public static class ErrorCodes
{
    public const string ValidationFailed = "validation_failed";
    public const string NotFound = "not_found";
    public const string ConcurrencyConflict = "concurrency_conflict";
    public const string DuplicateResource = "duplicate_resource";
    public const string CapacityFull = "capacity_full";
    public const string PrereqNotMet = "prereq_not_met";
    public const string ScheduleConflict = "schedule_conflict";
    public const string RateLimited = "rate_limited";
    public const string TransientDbError = "transient_db_error";
    public const string DatabaseUnavailable = "database_unavailable";
    public const string Unknown = "unknown_error";

    public const string IdempotencyKeyConflict = "idempotency_key_conflict";
    public const string IdempotencyKeyMissing = "idempotency_key_missing";
}
