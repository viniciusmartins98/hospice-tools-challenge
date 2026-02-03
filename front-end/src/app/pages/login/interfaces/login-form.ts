import { FormControl } from "@angular/forms";

export default interface ILoginForm {
  username: FormControl<string>;
  password: FormControl<string>;
}