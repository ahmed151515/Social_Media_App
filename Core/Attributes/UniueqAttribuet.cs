using Core.Interfaces.Repository;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Core.Attributes
{
	public class UniueqNameCommunityAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value is string name && validationContext.ObjectInstance is Community community)
			{
				var communityRepository = validationContext.GetService<ICommunityRepository>();
				if (communityRepository is not null)
				{
					var res = communityRepository.GetAll().Any(e => e.Id != community.Id && e.Name == name);
					if (res == false)
					{
						return ValidationResult.Success;
					}
				}

			}
			return new ValidationResult(ErrorMessage);

		}
	}
}
