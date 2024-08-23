const ProductTagTop = ({ tag }) => {
  function getTagColor(tag) {
    const colors = {
      'BestSeller': 'bg-indigo-600',
      'FlashDeal': 'bg-red-600',
    }
    return colors[tag.category];
  }
  return (
    <span className={`${getTagColor(tag)} p-2 rounded-sm text-xs text-white font-bold`}>{tag.text}</span>
  )}

export default ProductTagTop
