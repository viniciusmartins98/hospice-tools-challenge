import { GenderEnum } from "../enums/gender.enum";
import IColor from "../../colors/interfaces/color";

export default interface IPatient {
  id: string;
  firstName: string;
  lastName: string;
  fullName: string;
  gender: GenderEnum | null;
  age: number | null;
  favoriteColor: IColor | null;
  createdAt: Date;
  updatedAt: Date;
}