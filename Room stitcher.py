import xml.etree.ElementTree as ET
import os
import pygame



#screen
swid = 1280
shei = 768
screen = pygame.display.set_mode((swid, shei))
pygame.display.set_caption('Room Stitcher')

pygame.init()

#color
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
GRAY = (127, 127, 127)
GREEN = (0, 255, 0)

#variables
files = []
buttons = []
rooms = []
current_id = 0
mouse = {'x':0, 'y':0, 'gx':0, 'gy':0, 'click':False, 'hold':False}
mouse_count = 0

up = 0
down = 0
right = 0
left = 0
camera = {'x':0, 'y':0}

clock = pygame.time.Clock()

#bubble sort
def bubbleSort(lii):
    arr = lii
    n = len(arr)
    for i in range(n):
        for j in range(0, n-i-1):
            if arr[j] > arr[j+1] :
                arr[j], arr[j+1] = arr[j+1], arr[j]
    return arr

#create room list
for file in os.listdir(os.getcwd()):
    if file.endswith(".xml"):
        files.append(file)

#room object
class room:
    def __init__(self, _name):
        self.tree = ET.parse(_name)
        self.root = self.tree.getroot()
        
        self.x = 0
        self.y = 0
        self.width = int(self.root[1].attrib['width'])
        self.height = int(self.root[1].attrib['height'])

        self.map = self.root[1][0].text.split(',')
        for i in range(len(self.map)):
            self.map[i] = int(self.map[i])

        self.mouse_control = False
        self.m_c = 0

        self.placed = False

    def draw(self):
        for i in range(len(self.map)):
            if self.mouse_control:
                self.x = mouse['gx']
                self.y = mouse['gy']
                tile_x = int(i % self.width) * 8 + self.x
                tile_y = int(i / self.width) * 8 + self.y
            else:
                tile_x = int(i % self.width) * 8 + self.x + camera['x']
                tile_y = int(i / self.width) * 8 + self.y + camera['y']
            if tile_x > 79 and tile_x < swid and tile_y > -1 and tile_y < shei:
                if self.map[i] == 0:
                    pygame.draw.rect(screen, GRAY, (tile_x, tile_y, 8, 8))
                else:
                    pygame.draw.rect(screen, WHITE, (tile_x, tile_y, 8, 8))

class button:
    def __init__(self, _room_id):
        self.room_id = _room_id

        self.x = int(self.room_id % 4) * 16 + 8
        self.y = int(self.room_id / 4) * 16 + 8
        self.width = 8
        self.height = 8

        self.is_mouse_on = False

        self.color = GRAY

    def draw(self):
        self.is_mouse_on = mouse['x'] > self.x and mouse['y'] > self.y and mouse['x'] < self.x + self.width and mouse['y'] < self.y + self.height
        if rooms[self.room_id].mouse_control:
            self.color = WHITE
        elif self.is_mouse_on:
            self.color = WHITE
        else:
            self.color = GRAY
        pygame.draw.rect(screen, self.color, (self.x, self.y, self.width, self.height))


class fin_but:
    def __init__(self):
        self.x = 0
        self.y = shei - 16
        self.width = 72
        self.height = 16
        
        self.is_mouse_on = False

        self.color = GREEN

    def draw(self):
        self.is_mouse_on = mouse['x'] > self.x and mouse['y'] > self.y and mouse['x'] < self.x + self.width and mouse['y'] < self.y + self.height
        if self.is_mouse_on:
            self.color = WHITE
        else:
            self.color = GREEN
        pygame.draw.rect(screen, self.color, (self.x, self.y, self.width, self.height))

fin_b = fin_but()


#create room
for i in range(len(files)):
    new_room = room(files[i])
    rooms.append(new_room)
    new_button = button(i)
    buttons.append(new_button)

rooms[current_id].mouse_control = True
    

#main loop
running = True
building = False
frame_count = 0
frame_count_max = 1
while running:

    #update frame count
    frame_count += 1
    if frame_count > frame_count_max:
        frame_count = 0
    
    #update event
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
            building = False
        elif event.type == pygame.MOUSEBUTTONUP:
            mouse['hold'] = False
        elif event.type == pygame.MOUSEBUTTONDOWN:
            mouse['hold'] = True
        elif event.type == pygame.KEYDOWN:
            if event.key == pygame.K_UP:
                up = 1
            if event.key == pygame.K_DOWN:
                down = 1
            if event.key == pygame.K_LEFT:
                left = 1
            if event.key == pygame.K_RIGHT:
                right = 1
        elif event.type == pygame.KEYUP:
            if event.key == pygame.K_UP:
                up = 0
            if event.key == pygame.K_DOWN:
                down = 0
            if event.key == pygame.K_LEFT:
                left = 0
            if event.key == pygame.K_RIGHT:
                right = 0

    #update mouse
    if mouse['hold']:
        mouse_count += 1
    else:
        mouse_count = 0
    mouse['click'] = mouse_count == 1

    mouse['x'], mouse['y'] = pygame.mouse.get_pos()
    mouse['gx'] = int(mouse['x'] / 8) * 8
    mouse['gy'] = int(mouse['y'] / 8) * 8

    #update camera
    if frame_count == frame_count_max:
        if up == 1:
            camera['y'] += 8
        if down == 1:
            camera['y'] -= 8
        if left == 1:
            camera['x'] += 8
        if right == 1:
            camera['x'] -= 8

    #if mouse click then place room
    if mouse['click'] and mouse['x'] > 79 and current_id > -1:
        rooms[current_id].mouse_control = False
        rooms[current_id].placed = True
        rooms[current_id].x -= camera['x']
        rooms[current_id].y -= camera['y']
        current_id = -1

    #if press green button then build the map
    if fin_b.is_mouse_on and mouse['hold']:
        building = True
        running = False

    #draw
    screen.fill(BLACK)

    for i in range(len(rooms)):
        if rooms[i].placed or rooms[i].mouse_control:
            rooms[i].draw()

        if buttons[i].is_mouse_on and mouse['hold']:
            current_id = -1
            for k in range(len(rooms)):
                rooms[k].mouse_control = False
            current_id = i
            rooms[current_id].mouse_control = True
            rooms[current_id].placed = False
        buttons[i].draw()

    fin_b.draw()

    pygame.draw.rect(screen, WHITE, (72, 0, 8, shei))

    #print (str(running) + ' ' + str(building))

    pygame.display.update()
    clock.tick(60)



#Build the map
x_list = []
y_list = []
x_width_list = []
y_height_list = []
arr_data = {'width': 0, 'height': 0}
map_data = []
if building:
    for abcd in range(len(rooms)):
        if rooms[abcd].placed:
            x_list.append(rooms[abcd].x / 8)
            y_list.append(rooms[abcd].y / 8)
            x_width_list.append(rooms[abcd].x / 8 + rooms[abcd].width)
            y_height_list.append(rooms[abcd].y / 8 + rooms[abcd].height)

    x_list = bubbleSort(x_list)
    y_list = bubbleSort(y_list)
    x_width_list = bubbleSort(x_width_list)
    y_height_list = bubbleSort(y_height_list)

    print(x_list)
    print(y_list)
    print(x_width_list)
    print(y_height_list)

    arr_data['width'] = x_width_list[len(x_width_list) - 1] - x_list[0]
    arr_data['height'] = y_height_list[len(y_height_list) - 1] - y_list[0]

    print(arr_data)

    for i in range(int(arr_data['width'] * arr_data['height'])):
        map_data.append(0)

    num_of_room_placed = 0
    for i in range(len(rooms)):
        if rooms[i].placed:
            num_of_room_placed += 1
            rooms[i].x = rooms[i].x / 8
            rooms[i].y = rooms[i].y / 8


    for i in range(len(rooms)):
        if rooms[i].placed:
            for k in range(len(rooms[i].map)):
                te_ro_x = int(k % rooms[i].width) + rooms[i].x - x_list[0]
                te_ro_y = int(k / rooms[i].width) + rooms[i].y - y_list[0]
                num_in_list = int(arr_data['width'] * te_ro_y + te_ro_x)
                map_data[num_in_list] = rooms[i].map[k]

    root = ET.Element('XnaContent')
    
    asset = ET.SubElement(root, 'Asset')
    wid = ET.SubElement(asset, 'Width')
    hei = ET.SubElement(asset, 'Height')
    b_size = ET.SubElement(asset, 'BlockSize')
    roooo = ET.SubElement(asset, 'Rooms')
    map_ = ET.SubElement(asset, 'BlockData')
    
    asset.set('Type', 'AdelieEngine.Data.MapData')
    
    wid.text = str(float(arr_data['width']))
    hei.text = str(float(arr_data['height']))
    b_size.text = str(float(8))

    room_list_xml = []

    for i in range(len(rooms)):
        if rooms[i].placed:
            room_list_xml.append(ET.SubElement(roooo, 'Item'))
            room_list_xml[len(room_list_xml) - 1].text = str(int(rooms[i].x - x_list[0])) + " " + str(int(rooms[i].y - y_list[0])) + " " + str(int(rooms[i].width)) + " " + str(int(rooms[i].height))

    real_data_in_str = ''
    for i in range(len(map_data)):
        if i == 0:
            real_data_in_str += str(int(map_data[0]))
        else:
            real_data_in_str += ' ' + str(int(map_data[i]))

    map_.text = real_data_in_str




    #Create xml
    mydata = ET.tostring(root, encoding="unicode", method="xml")
    myfile = open("newdata.xml", "w")
    myfile.write(mydata)

    
    
            
        
    


pygame.quit()

