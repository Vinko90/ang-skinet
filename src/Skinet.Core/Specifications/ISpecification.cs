using System.Linq.Expressions;

namespace Skinet.Core.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    
    List<Expression<Func<T, object>>> Includes { get; }
    
    //For ThenInclude operators
    List<string> IncludeStrings {get;}
    
    Expression<Func<T, object>> OrderBy { get; }
    
    Expression<Func<T, object>> OrderByDescending { get; }
    
    int Take { get; }
    
    int Skip { get; }
    
    bool IsPagingEnabled { get; }
}
