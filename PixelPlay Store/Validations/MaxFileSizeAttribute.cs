namespace PixelPlay.Validations
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxfilesize;
        public MaxFileSizeAttribute(int allowedmaxfilesize)
        {
            maxfilesize = allowedmaxfilesize;
        }

        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            var file = value as IFormFile;
            //var file = Convert.ChangeType(value, IFormFile);

            if (file is not null)
            {
                if(file.Length > maxfilesize)
                {
                    return new ValidationResult(errorMessage: $"Maximum allowed size is {maxfilesize} bytes!");
                }       
            }
            return ValidationResult.Success;
        }
    }
}

