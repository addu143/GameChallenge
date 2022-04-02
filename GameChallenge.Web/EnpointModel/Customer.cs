using System.ComponentModel.DataAnnotations;

namespace GameChallenge.Web.EnpointModel
{
    public class CustomerRegisterRequest
    {
        [MaxLength(200)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }

    public class CustomerLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }
}
