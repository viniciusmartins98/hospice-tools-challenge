import IColor from '../../colors/interfaces/color';
import IAgeRange from './age-range';

export default interface IPatientFavoriteColorCountByAgeRange {
  patientsCount: number;
  ageRange: IAgeRange;
  favoriteColor: IColor;
}
