// using System.ComponentModel.DataAnnotations;
// using tl2_tp10_2023_alvaroad29.Models;


// namespace tl2_tp10_2023_alvaroad29.Attributes
// {
//     public class UsernameValidation : ValidationAttribute
//     {
//         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//         {
//             var _usuarioRepository = validationContext.GetRequiredService<IUsuarioRepository>();
//             if (value != null)
//             {
//                 string stringValue = value.ToString();

//                 if (_usuarioRepository.ExistUser(stringValue))
//                 {
//                     return new ValidationResult("El nombre de usuario ingresado ya existe.");
//                 }
//             }
//             return ValidationResult.Success;
//         }
//     }
// }