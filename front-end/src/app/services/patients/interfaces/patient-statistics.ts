import IPatientCountByColor from './patient-count-by-color';
import IPatientFavoriteColorCountByAgeRange from './patient-favorite-color-count-by-age-range';

export default interface IPatientStatistics {
  patientsCount: number;
  patientsWithColorCount: number;
  patientsWithNoColorsCount: number;
  patientsCountByColor: IPatientCountByColor[];
  favoriteColorPatientsCountByAgeRange: IPatientFavoriteColorCountByAgeRange[];
}
