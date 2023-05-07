using System.Linq.Expressions;

namespace Skinet.Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    
    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();
    
    public List<string> IncludeStrings { get; } = new();
    
    public Expression<Func<T, object>> OrderBy { get; private set; }
    
    public Expression<Func<T, object>> OrderByDescending { get; private set; }
    
    public int Take { get; private set; }
    
    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    
    /// <summary>
    /// Then finally in order to use this you would (in your specification) you would pass a string instead of the expression e.g:
    /// AddInclude(c => c.CountryOfBirth);
    /// AddInclude(“CountyOfOrigin.DefaultCountryLanguage”);
    /// You can chain on as many ThenIncludes as you wish separating each one with a period.
    /// Obviously you need to be careful with spelling as this is string based.
    /// </summary>
    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExp)
    {
        OrderBy = orderByExp;
    }
    
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExp)
    {
        OrderByDescending = orderByDescExp;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}
