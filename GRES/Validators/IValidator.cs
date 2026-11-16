using System;

namespace GRES.Validators;

internal interface IValidator<TEntity>
{
    ValidatorResultValue Validate(TEntity entity);
}
