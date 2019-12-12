export const normalizeWith = getId => (data = []) => {
  if (!(getId instanceof Function)) {
    throw new Error("getId debe ser una función.");
  }

  if (!(data instanceof Array)) {
    throw new Error("Solo se normalizan arrays.");
  }

  return data.reduce((acc, value) => ({ ...acc, [getId(value)]: value }), {});
};

export const normalize = normalizeWith(x => x.id);
