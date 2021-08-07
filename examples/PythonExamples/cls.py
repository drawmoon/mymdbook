class UserService:
  name: str

  def __init__(self, name: str):
    print("UserService init")
    self.name = name

  def __call__(self, todo: str):
    print("UserService call")
    print(todo)

  def __del__(self):
    print("UserService del")

  def get_name(self) -> str:
    return self.name

user_service = UserService("Aniya Ford")
user_service("run")
name = user_service.get_name()
print(name)
