export default interface IPatient {
  id: string;
  firstName: string;
  lastName: string;
  fullName: string;
  gender: string;
  age: number;
  favoriteColor: string;
  createdAt: Date;
  updatedAt: Date;
}