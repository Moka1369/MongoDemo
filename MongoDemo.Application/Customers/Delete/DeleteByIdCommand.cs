using MediatR;

namespace MongoDemo.Application.Customers.Delete;

public record DeleteByIdCommand(string Id):IRequest<bool>;