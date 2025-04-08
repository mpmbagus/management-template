using System.Collections;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MANAGEMENT.HELPER.Helpers;

public enum ResponseStatus
{
    Success = 1,
    Error = 0,
}

public class JsonResponse
{
    private class ResponseModel
    {
        public ResponseModel(ResponseStatus status, string message, object? data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
    }

    private class PaginatedResponseModel : ResponseModel
    {
        public PaginatedResponseModel(
            ResponseStatus status,
            string message,
            int page,
            int size,
            object data
        )
            : base(status, message, data)
        {
            Page = page;
            Size = size;
        }

        public int Page { get; set; }
        public int Size { get; set; }
    }

    private ResponseModel Model;
    private HttpStatusCode StatusCode;

    public JsonResponse(
        ResponseStatus status,
        string message,
        object? data,
        HttpStatusCode statusCode = HttpStatusCode.OK
    )
    {
        Model = new ResponseModel(status, message, data);
        StatusCode = statusCode;
    }

    private JsonResponse(ResponseModel model, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        Model = model;
        StatusCode = statusCode;
    }

    public ContentResult GetResponse()
    {
        var response = new ContentResult
        {
            StatusCode = (int)StatusCode,
            ContentType = "application/json",
        };

        if (StatusCode != HttpStatusCode.NoContent)
        {
            response.Content = JsonConvert.SerializeObject(Model);
        }

        return response;
    }

    public static ContentResult Ok(object? data, string message = "Success")
    {
        return new JsonResponse(ResponseStatus.Success, message, data).GetResponse();
    }

    public static ContentResult CreatedAtAction(
        object? data,
        string message = "Insert Success",
        HttpStatusCode statusCode = HttpStatusCode.Created
    )
    {
        return new JsonResponse(ResponseStatus.Success, message, data).GetResponse();
    }

    public static ContentResult ExecuteSuccess(
        string message = "",
        HttpStatusCode code = HttpStatusCode.NoContent
    )
    {
        return new JsonResponse(
            ResponseStatus.Success,
            "Execute Success." + message,
            new { },
            code
        ).GetResponse();
    }

    public static ContentResult ExecuteFailed(
        string message = "",
        HttpStatusCode code = HttpStatusCode.InternalServerError
    )
    {
        return new JsonResponse(
            ResponseStatus.Error,
            "Execute Failed. " + message,
            new { },
            code
        ).GetResponse();
    }

    public static ContentResult PaginatedOk(
        int page,
        int size,
        object data,
        string message = "Success"
    )
    {
        return new JsonResponse(
            new PaginatedResponseModel(ResponseStatus.Success, message, page, size, data)
        ).GetResponse();
    }

    public static ContentResult Error(
        string message,
        HttpStatusCode errorCode = HttpStatusCode.InternalServerError
    )
    {
        return new JsonResponse(
            ResponseStatus.Error,
            message,
            new ArrayList(),
            errorCode
        ).GetResponse();
    }

    public static ContentResult DataNotFound(
        string message = "Data Not Found",
        HttpStatusCode errorCode = HttpStatusCode.OK,
        bool array = false
    )
    {
        return new JsonResponse(
            ResponseStatus.Error,
            message,
            array ? new { } : new ArrayList(),
            errorCode
        ).GetResponse();
    }

    public static ContentResult ParameterIncorrect(
        string message = "Parameter Incorrect",
        HttpStatusCode errorCode = HttpStatusCode.BadRequest
    )
    {
        return new JsonResponse(
            ResponseStatus.Error,
            message,
            new ArrayList(),
            errorCode
        ).GetResponse();
    }

    public static ContentResult Unauthorized(
        string message,
        HttpStatusCode code = HttpStatusCode.Unauthorized
    )
    {
        return new JsonResponse(ResponseStatus.Error, message, new { }, code).GetResponse();
    }
}
