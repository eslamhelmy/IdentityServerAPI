namespace UserIdentity.Records{
    public record UserDto(string UserId, string UserName, bool IsApproved,string UserType=""){
        public string UserType {get; set;} = UserType?? string.Empty;
    }

}
