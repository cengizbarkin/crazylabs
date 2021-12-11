using Colors;
using Drops;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Ui
{
    public class CounterObject : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text value;
        [SerializeField] private SpriteAtlas dropSpriteAtlas;

        private int _value;
        
        private void Start()
        {
            image.sprite = dropSpriteAtlas.GetSprite(gameObject.name);
            DropFactory.DropsAreClosedAction += DropsAreClosedAction;
            GameManager.RestartGame += RestartGameAction;
        }

        private void DropsAreClosedAction(IDropColor color, int count)
        {
            if (color.GetType().Name == gameObject.name)
            {
                _value += count;
                value.text = _value.ToString();
            }
        }

        private void RestartGameAction()
        {
            _value = 0;
            value.text = _value.ToString();
        }
        
        private void OnDestroy()
        {
            DropFactory.DropsAreClosedAction -= DropsAreClosedAction;
            GameManager.RestartGame -= RestartGameAction;
        }
    }
}
