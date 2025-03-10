namespace PixelPlay.Validations
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string extensions;
        public AllowedExtensionsAttribute(string allowedextensions)
        {
            extensions = allowedextensions;
        }

        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            var file = value as IFormFile;
            //var file = Convert.ChangeType(value, IFormFile);

            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isallowed = extensions.Split(separator: ",").Contains(extension, StringComparer.OrdinalIgnoreCase);

                if (!isallowed)
                {
                    return new ValidationResult(errorMessage: $"Only {extensions} are allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
