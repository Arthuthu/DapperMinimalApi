using FluentValidation;

namespace MiniumApiDemo;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/Users", GetUsers).AllowAnonymous();
        app.MapGet("/Users/{id}", GetUser).AllowAnonymous();
        app.MapPost("/Users", InsertUser).AllowAnonymous();
        app.MapPut("/Users", UpdateUser).AllowAnonymous();
        app.MapDelete("/Users", DeleteUser).AllowAnonymous();
    }

    private static async Task<IResult> GetUsers(IUserData data)
    {
		try
		{
            return Results.Ok(await data.GetUsers());
		}
		catch (Exception ex)
		{
            return Results.Problem(ex.Message);
		}
    }

    private static async Task<IResult> GetUser(int id, IUserData data)
    {
        try
        {
            var results = await data.GetUser(id);

            if (results is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertUser(UserModel user, IValidator<UserModel> validator, IUserData data)
    {
        try
        {  
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
                return Results.BadRequest(errors);
            }

            await data.InsertUser(user);

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateUser(UserModel user, IUserData data)
    {
        try
        {
            await data.UpdateUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteUser(int id, IUserData data)
    {
        try
        {
            await data.DeleteUser(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
