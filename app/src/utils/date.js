export function formatDate(dateStr) {
  const d = new Date(dateStr);
  if (isNaN(d)) return "";

  const dd = String(d.getDate()).padStart(2, "0");
  const mm = String(d.getMonth() + 1).padStart(2, "0");
  const yyyy = d.getFullYear();

  return `${dd}-${mm}-${yyyy}`;
}

export function formatDateTime(dateStr) {
  const d = new Date(dateStr);
  if (isNaN(d)) return "";

  const dd = String(d.getDate()).padStart(2, "0");
  const mm = String(d.getMonth() + 1).padStart(2, "0");
  const yyyy = d.getFullYear();

  const hh = String(d.getHours()).padStart(2, "0");
  const min = String(d.getMinutes()).padStart(2, "0");
  const ss = String(d.getSeconds()).padStart(2, "0");

  return `${dd}-${mm}-${yyyy} ${hh}:${min}:${ss}`;
}

export function calculateAge(dateStr) {
  const d = new Date(dateStr);
  if (isNaN(d)) return "";

  const today = new Date();
  let age = today.getFullYear() - d.getFullYear();

  const hasHadBirthdayThisYear =
    today.getMonth() > d.getMonth() ||
    (today.getMonth() === d.getMonth() && today.getDate() >= d.getDate());

  if (!hasHadBirthdayThisYear) {
    age -= 1;
  }

  return age;
}
