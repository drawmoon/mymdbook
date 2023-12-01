/**
 * A version number.
 */
export interface IVersion {
  /**
   * Gets the value of the major component of the version number for the current Version object.
   */
  readonly major: number;

  /**
   * Gets the value of the minor component of the version number for the current Version object.
   */
  readonly minor: number;

  /**
   * Gets the value of the build component of the version number for the current Version object.
   */
  readonly build: number;

  /**
   * Gets String representation.
   */
  string(): string;
}
