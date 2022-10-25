namespace ooapi.v5.core.Repositories;
public class BaseRepository

{

    protected readonly CoreDBContext DbContext = null;



    public BaseRepository(CoreDBContext dbContext)

    {
        this.DbContext = dbContext;
    }


}
