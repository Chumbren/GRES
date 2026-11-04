using System;

namespace GRES.Validators;

internal interface IValidator<TEntity>
{
    bool Validate(TEntity entity);
}
