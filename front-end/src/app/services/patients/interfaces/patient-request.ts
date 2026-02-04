import { GenderEnum } from "../enums/gender.enum";

export default interface IPatientRequest {
  firstName: string;
  lastName: string;
  gender: GenderEnum | null;
  age: number | null;
  favoriteColorId: string | null;
}