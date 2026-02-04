import { GenderEnum } from '../enums/gender.enum';
import IPatientCountByColor from './patient-count-by-color';

export default interface IPatientCountByColorAndGender extends IPatientCountByColor {
  gender: GenderEnum;
}
