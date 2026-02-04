import IColor from "./color";

export default interface IPatient {
  id: string;
  firstName: string;
  lastName: string;
  fullName: string;
  gender: string;
  age: number;
  favoriteColor: IColor;
  createdAt: Date;
  updatedAt: Date;
}