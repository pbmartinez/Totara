using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    /// <summary>
    /// The custom exception for validation errors
    /// </summary>
    public class ApplicationValidationErrorsException : Exception
    {
        private static readonly string EXCEPTION_ERROR_MESSAGE = "Error de validación, compruebe los errores de la validación para más información";
        #region Properties

        /// <summary>
        /// Get or set the validation errors messages
        /// </summary>
        public IEnumerable<string> ValidationErrors { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Create new instance of Application validation errors exception
        /// </summary>
        /// <param name="validationErrors">The collection of validation errors</param>
        public ApplicationValidationErrorsException(IEnumerable<string> validationErrors)
            : base(message: EXCEPTION_ERROR_MESSAGE)
        {
            ValidationErrors = validationErrors;
        }

        #endregion
    }
}
