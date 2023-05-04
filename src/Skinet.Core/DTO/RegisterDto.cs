using System.ComponentModel.DataAnnotations;

namespace Skinet.Core.DTO;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    /// <summary>
    /// This regular expression match can be used for validating strong password.
    /// It expects at least 1 small-case letter, 1 Capital letter, 1 digit, 1 special character
    /// and the length should be between 6-10 characters.
    /// The sequence of the characters is not important.
    /// This expression follows the above 4 norms specified by microsoft for a strong password.
    /// </summary>
    [Required]
    [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 Number, 1 Non-Alphanumeric and at least 6 characters")]
    public string Password { get; set; }
}
