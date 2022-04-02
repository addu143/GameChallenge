using GameChallenge.Core.DBEntities;

namespace GameChallenge.Web.Helpers
{
    public interface IResponseGeneric
    {
        string Code { get; set; }
        string Message { get; set; }
        dynamic Result { get; set; }

        ResponseGeneric Error(string message = "Something went wrong", dynamic result = null, Log log = null);
        ResponseGeneric Success(string message = "Successfull", dynamic result = null, Log log = null);
    }
}