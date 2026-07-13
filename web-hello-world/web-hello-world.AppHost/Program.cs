var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.web_hello_world>("web-hello-world");

builder.Build().Run();
