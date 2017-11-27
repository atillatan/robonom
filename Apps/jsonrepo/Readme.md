# JSON Repository

Purpose of Json Repository is retrive some json data from /wwwroot/App_Data/jsonrepo/menu.json

## Define Sevice

define in Startup.cs 

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IJsonRepository, JsonRepositoryImpl>();
    }

}
```

put service referance in model class

```csharp

public class JsonModel {

    private IJsonRepository _repo;

    private IList<dynamic> _data;

    public JsonModel{
        _repo=new JsonRepositoryImple();
        _data=repo.getAllData();
    }

    public IList<dynamic> Data{
        get { return _data;}
    }
}

```

and use it in razor page

```csharp
@foreach(var item in Model.Data){
   <p>item.name</p>
}

```