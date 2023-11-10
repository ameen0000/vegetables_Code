
using LoginAndVegitable.Models;
using LoginAndVegitable.Utilities;


    public interface IAuthservice
    {
        public userreaponse Addlist(userreaponse userapi);

    public string CreateToken(userreaponse userreaponse);

    public string Verify(userreaponse login);

    public List<userreaponse> get();

}

