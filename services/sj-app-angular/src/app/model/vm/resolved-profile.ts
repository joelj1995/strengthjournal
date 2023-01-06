import { Country } from "../country";
import { ProfileSettings } from "../profile-settings";

export interface ResolvedProfile {
  settings: ProfileSettings;
  countryList: Country[];
}